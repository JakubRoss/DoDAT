<script setup>
import { ref } from "vue";
import api from "@/axios";
import { useRouter } from "vue-router";

const router = useRouter();

// Funkcja logout
const logout = async () => {
  try {
    const response = await api.post("/api/account/logout");

    if (response.status === 200) {
      alert("Pomyślnie wylogowano!");
      router.push("/login");
    }
  } catch (error) {
    console.error("Logout failed", error);
    alert("Błąd podczas wylogowywania");
  }
};
</script>

<template>
  <!-- Przycisk wylogowania -->
  <button @click="logout">Wyloguj</button>
</template>

<style scoped>
button {
  padding: 10px 20px;
  background-color: #ff5c5c;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s;
  margin-left: auto;
}

button:hover {
  background-color: #e04e4e;
}

button:focus {
  outline: none;
}

button:active {
  background-color: #d14a4a;
}
</style>
