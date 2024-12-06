<script setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import api from "@/axios";

// Reaktywny stan logowania
const isLoggedIn = ref(false);
const router = useRouter();

// Sprawdzanie statusu logowania
const checkLoginStatus = async () => {
  try {
    const response = await api.get("/api/account/isLoggedIn");
    isLoggedIn.value = response.data.isLoggedIn;
  } catch (error) {
    console.error("Błąd podczas sprawdzania logowania:", error);
    isLoggedIn.value = false;
  }
};

// Ustawianie stanu logowania
const setLoginStatus = (status) => {
  isLoggedIn.value = status;
};

// Funkcja do wylogowania
const logout = async () => {
  try {
    await api.post("/api/account/logout");
    isLoggedIn.value = false;
    router.push("/login");
  } catch (error) {
    console.error("Błąd wylogowywania:", error);
  }
};

// Sprawdzanie statusu logowania po załadowaniu komponentu
onMounted(() => {
  checkLoginStatus();
});
</script>

<template>
  <header>
    <nav class="navbar">
      <div class="navbar-content">
        <button v-if="isLoggedIn" @click="logout" class="nav-btn">
          Logout
        </button>
        <button v-else @click="router.push('/register')" class="nav-btn">
          Register
        </button>
      </div>
    </nav>
  </header>

  <!-- przekazanie setLoginStatus jako props do router-view -->
  <router-view
    :onLoginSuccess="setLoginStatus"
    style="margin-top: 5vh; padding-bottom: 10vh"
  />
</template>

<style scoped>
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  background-color: #333;
  color: white;
  font-size: 18px;
}

.navbar-content {
  display: flex;
  align-items: center;
}

.nav-btn {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s;
  margin-left: auto;
}

.nav-btn:hover {
  background-color: #0056b3;
}
</style>
