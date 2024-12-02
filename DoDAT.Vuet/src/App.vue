<script setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import LogoutButton from "@/components/LogoutButton.vue";

const isLoggedIn = ref(false);

const checkLoginStatus = () => {
  isLoggedIn.value = document.cookie.includes("AspNetCore.Cookies");
};

onMounted(() => {
  checkLoginStatus();
});
</script>

<template>
  <header>
    <nav class="navbar">
      <div class="navbar-content">
        <LogoutButton v-if="isLoggedIn" />
      </div>
    </nav>
  </header>

  <router-view />
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
