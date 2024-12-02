<template>
  <div class="home">
    <h1>To-Do List</h1>

    <!-- Formularz filtrowania zadań po dacie -->
    <div class="date-filter">
      <label for="selectedDate">Filter by Date:</label>
      <input type="date" v-model="selectedDate" @change="fetchTasksByDate" />
      <button @click="resetFilter" class="btn btn-reset">Reset</button>
    </div>

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
          <tr
            v-for="task in tasks"
            :key="task.id"
            :class="{ 'task-completed': task.isCompleted }"
          >
            <td>{{ task.title }}</td>
            <td>{{ task.description }}</td>
            <td>{{ formatDate(task.dueDate) }}</td>
            <td>{{ task.isCompleted ? "Yes" : "No" }}</td>
            <td>
              <button @click="editTask(task)" class="btn btn-edit">Edit</button>
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
          <textarea v-model="newTask.description"></textarea>
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

    <!-- Formularz edytowania zadania -->
    <div v-if="showEditTaskForm" class="edit-task-form">
      <h2>Edit Task</h2>
      <form @submit.prevent="updateTask">
        <div>
          <label for="title">Title:</label>
          <input type="text" v-model="editTaskData.title" required />
        </div>
        <div>
          <label for="description">Description:</label>
          <textarea v-model="editTaskData.description"></textarea>
        </div>
        <div>
          <label for="dueDate">Due Date:</label>
          <input type="date" v-model="editTaskData.dueDate" required />
        </div>
        <div>
          <label for="isCompleted">Is Completed:</label>
          <input type="checkbox" v-model="editTaskData.isCompleted" />
        </div>
        <div>
          <button type="submit">Save Changes</button>
          <button type="button" @click="cancelEditTask">Cancel</button>
        </div>
      </form>
    </div>

    <!-- Przycisk do otwarcia formularza -->
    <div v-if="!showAddTaskForm && !showEditTaskForm" class="add-task-button">
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
const showAddTaskForm = ref(false);
const showEditTaskForm = ref(false);
const newTask = ref({
  title: "",
  description: "",
  dueDate: new Date().toISOString().slice(0, 10), // Domyślna data na dziś
});
const editTaskData = ref({
  id: null,
  title: "",
  description: "",
  dueDate: "",
  isCompleted: false,
});
const selectedDate = ref(null);

// Funkcja do pobrania wszystkich zadań lub zadań po dacie
const fetchTasks = async () => {
  try {
    const response = await api.get("/api/tasks");
    tasks.value = response.data;
  } catch (error) {
    console.error("Error fetching tasks:", error);
  }
};

// Funkcja do pobrania zadań po dacie
const fetchTasksByDate = async () => {
  try {
    if (selectedDate.value) {
      const response = await api.get("/api/tasks/by-date", {
        params: { selectedDate: selectedDate.value },
      });
      tasks.value = response.data;
    } else {
      await fetchTasks(); // Jeśli nie wybrano daty, ładuj wszystkie zadania
    }
  } catch (error) {
    console.error("Error fetching tasks by date:", error);
  }
};

// Funkcja do edytowania zadania
const editTask = (task) => {
  editTaskData.value = { ...task };
  editTaskData.value.dueDate = task.dueDate.slice(0, 10);
  showEditTaskForm.value = true;
  showAddTaskForm.value = false;
};

// Funkcja do usuwania zadania
const deleteTask = async (taskId) => {
  try {
    await api.delete(`/api/tasks/${taskId}`);
    await fetchTasks();
    alert("Task deleted successfully");
  } catch (error) {
    console.error("Error deleting task:", error);
  }
};

// Funkcja do dodawania nowego zadania
const createTask = async () => {
  try {
    await api.post("/api/tasks", newTask.value);
    await fetchTasks();
    showAddTaskForm.value = false;
    newTask.value = {
      title: "",
      description: "",
      dueDate: new Date().toISOString().slice(0, 10),
    }; // Resetuj formularz z domyślną datą
    alert("Task added successfully");
  } catch (error) {
    console.error("Error adding task:", error);
  }
};

// Funkcja do aktualizacji zadania
const updateTask = async () => {
  try {
    await api.put(`/api/tasks/${editTaskData.value.id}`, editTaskData.value);
    await fetchTasks();
    showEditTaskForm.value = false;
    alert("Task updated successfully");
  } catch (error) {
    console.error("Error updating task:", error);
    alert("There was an error updating the task.");
  }
};

// Funkcja do anulowania edytowania zadania
const cancelEditTask = () => {
  showEditTaskForm.value = false;
  editTaskData.value = {
    id: null,
    title: "",
    description: "",
    dueDate: "",
    isCompleted: false,
  };
};

// Funkcja do anulowania dodawania zadania
const cancelAddTask = () => {
  showAddTaskForm.value = false;
  newTask.value = {
    title: "",
    description: "",
    dueDate: new Date().toISOString().slice(0, 10),
  }; // Resetuj formularz
};

// Funkcja do resetowania filtra
const resetFilter = () => {
  selectedDate.value = null; // Resetuj wybraną datę
  fetchTasks(); // Pobierz wszystkie zadania
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
  margin-top: 20px;
  background-color: #28a745;
  color: white;
}

.btn-add:hover {
  background-color: #218838;
}

.add-task-form,
.edit-task-form {
  margin-top: 20px;
  padding: 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.add-task-form input,
.add-task-form textarea,
.edit-task-form input,
.edit-task-form textarea {
  width: 100%;
  padding: 10px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.add-task-form button,
.edit-task-form button {
  margin-right: 10px;
}

.task-completed {
  background-color: #d4edda;
}

.date-filter {
  margin-bottom: 20px;
}

.btn-reset {
  background-color: #ffc107;
  color: white;
}

.btn-reset:hover {
  background-color: #e0a800;
}
</style>
