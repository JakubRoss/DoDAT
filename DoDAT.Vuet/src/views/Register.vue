<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import api from "@/axios";

const router = useRouter();

const registerData = ref({
  Login: "",
  Password: "",
  ConfirmPassword: "",
});

const register = async () => {
  try {
    const { Login, Password, ConfirmPassword } = registerData.value;

    if (Password !== ConfirmPassword) {
      alert("Hasła muszą się zgadzać!");
      return;
    }

    await api.post("/api/account/register", { Login, Password });
    alert("Rejestracja zakończona sukcesem!");
    router.push("/login");
  } catch (error) {
    console.error("Błąd rejestracji:", error);
    alert("Błąd podczas rejestracji.");
  }
};
</script>

<template>
  <div class="auth-container">
    <form @submit.prevent="register">
      <input
        v-model="registerData.Login"
        type="text"
        placeholder="Login"
        required
      />
      <input
        v-model="registerData.Password"
        type="password"
        placeholder="Hasło"
        required
      />
      <input
        v-model="registerData.ConfirmPassword"
        type="password"
        placeholder="Potwierdź hasło"
        required
      />
      <button type="submit">Zarejestruj</button>
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
