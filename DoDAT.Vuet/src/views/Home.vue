<template>
  <div class="home">
    <h1>To-Do List</h1>

    <div v-if="tasks.length > 0">
      <table class="task-table">
        <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Dead Line</th>
            <th>Is Completed</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="task in tasks" :key="task.id">
            <td>{{ task.title }}</td>
            <td>{{ task.description }}</td>
            <td>{{ formatDate(task.dueDate) }}</td>
            <td>{{ task.isCompleted ? "Yes" : "No" }}</td>
            <td>
              <button @click="editTask(task.id)" class="btn btn-edit">
                Edit
              </button>
              <button @click="deleteTask(task.id)" class="btn btn-delete">
                Delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <div v-else>
      <p>No tasks available.</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import api from "@/axios";

// Reaktywne dane
const tasks = ref([]);

// Funkcja do pobrania wszystkich zadań
const fetchTasks = async () => {
  try {
    const response = await api.get("/api/tasks");
    tasks.value = response.data;
  } catch (error) {
    console.error("Error fetching tasks:", error);
  }
};

// Funkcja do formatowania daty
const formatDate = (dueDate) => {
  return dueDate ? dueDate.slice(0, 10) : "";
};

// Funkcja do edytowania zadania
const editTask = (taskId) => {
  console.log("Edit task with ID:", taskId);
};

// Funkcja do usuwania zadania
const deleteTask = async (taskId) => {
  try {
    await api.delete(`/api/tasks/${taskId}`);
    tasks.value = tasks.value.filter((task) => task.id !== taskId); // Usuń zadanie z lokalnej listy
    alert("Task deleted successfully");
  } catch (error) {
    console.error("Error deleting task:", error);
  }
};

// Pobierz zadania przy pierwszym załadowaniu komponentu
onMounted(fetchTasks);
</script>

<style scoped>
.home {
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
}

h2 {
  margin-bottom: 20px;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

th,
td {
  padding: 10px;
  text-align: left;
  border-bottom: 1px solid #ddd;
}

th {
  background-color: #f4f4f4;
}

button {
  padding: 5px 10px;
  margin: 0 5px;
  cursor: pointer;
  border: none;
  border-radius: 5px;
}

.btn-edit {
  background-color: #007bff;
  color: white;
}

.btn-edit:hover {
  background-color: #0056b3;
}

.btn-delete {
  background-color: #dc3545;
  color: white;
}

.btn-delete:hover {
  background-color: #c82333;
}
</style>
