<template>
  <div class="home">
    <h1>To-Do List</h1>

    <!-- Tabela z zadaniami -->
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

    <!-- Jeśli brak zadań -->
    <div v-if="tasks.length === 0">
      <p>No tasks available.</p>
    </div>

    <!-- Formularz dodawania nowego zadania -->
    <div v-if="showAddTaskForm" class="add-task-form">
      <h2>Add New Task</h2>
      <form @submit.prevent="createTask">
        <div>
          <label for="title">Title:</label>
          <input type="text" v-model="newTask.title" required />
        </div>
        <div>
          <label for="description">Description:</label>
          <textarea v-model="newTask.description" required></textarea>
        </div>
        <div>
          <label for="dueDate">Due Date:</label>
          <input type="date" v-model="newTask.dueDate" required />
        </div>
        <div>
          <button type="submit">Add Task</button>
          <button type="button" @click="cancelAddTask">Cancel</button>
        </div>
      </form>
    </div>

    <!-- Przycisk do otwarcia formularza -->
    <div v-if="!showAddTaskForm" class="add-task-button">
      <button @click="showAddTaskForm = true" class="btn btn-add">
        Add New Task
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import api from "@/axios";

// Reaktywne dane
const tasks = ref([]);
const showAddTaskForm = ref(false); // Zmienna do pokazania formularza
const newTask = ref({
  title: "",
  description: "",
  dueDate: "",
});

// Funkcja do pobrania wszystkich zadań
const fetchTasks = async () => {
  try {
    const response = await api.get("/api/tasks");
    tasks.value = response.data;
  } catch (error) {
    console.error("Error fetching tasks:", error);
  }
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

// Funkcja do dodawania nowego zadania
const createTask = async () => {
  try {
    const response = await api.post("/api/tasks", newTask.value);
    tasks.value.push(response.data); // Dodaj nowe zadanie do listy
    showAddTaskForm.value = false; // Ukryj formularz
    newTask.value = { title: "", description: "", dueDate: "" }; // Resetuj formularz
    alert("Task added successfully");
  } catch (error) {
    console.error("Error adding task:", error);
  }
};

// Funkcja do anulowania dodawania zadania
const cancelAddTask = () => {
  showAddTaskForm.value = false;
  newTask.value = { title: "", description: "", dueDate: "" }; // Resetuj formularz
};

// Pobierz zadania przy pierwszym załadowaniu komponentu
onMounted(fetchTasks);

// Funkcja do formatowania daty
const formatDate = (dueDate) => {
  return dueDate ? dueDate.slice(0, 10) : "";
};
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

.btn-add {
  background-color: #28a745;
  color: white;
}

.btn-add:hover {
  background-color: #218838;
}

.add-task-form {
  margin-top: 20px;
  padding: 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.add-task-form input,
.add-task-form textarea {
  width: 100%;
  padding: 10px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.add-task-form button {
  margin-right: 10px;
}

.add-task-button {
  margin-top: 20px;
}
</style>
