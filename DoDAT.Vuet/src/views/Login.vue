<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import api from "@/axios";

const router = useRouter();

const loginData = ref({
  Login: "",
  Password: "",
});

const login = async () => {
  try {
    const response = await api.post("/api/account/login", loginData.value);

    alert("Zalogowano pomyślnie!");
    router.push("/");
  } catch (error) {
    console.error("Błąd logowania:", error);
    alert("Nieprawidłowe dane logowania lub błąd serwera.");
  }
};
</script>

<template>
  <div class="auth-container">
    <form @submit.prevent="login">
      <input
        v-model="loginData.Login"
        type="text"
        placeholder="Login"
        required
      />
      <input
        v-model="loginData.Password"
        type="password"
        placeholder="Hasło"
        required
      />
      <button type="submit">Zaloguj</button>
    </form>
  </div>
</template>

<style scoped>
.auth-container {
  max-width: 400px;
  margin: 50px auto;
  padding: 20px;
  background: #fff;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

form {
  display: flex;
  flex-direction: column;
}

input {
  margin-bottom: 15px;
  padding: 10px;
  font-size: 16px;
}

button {
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}
</style>
