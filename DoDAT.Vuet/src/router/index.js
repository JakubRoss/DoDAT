import { createRouter, createWebHistory } from "vue-router";
import Login from "@/views/Login.vue";
import Home from "@/views/Home.vue";
import Register from "@/views/Register.vue"; // Dodajemy trasę rejestracji

function isLoggedIn() {
  // Sprawdzamy, czy istnieje ciasteczko sesji
  return document.cookie.includes("AspNetCore.Cookies");
}

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/login",
      name: "Login",
      component: Login,
      beforeEnter: (to, from, next) => {
        if (isLoggedIn()) {
          next("/home"); // Jeśli zalogowany, przekieruj na stronę główną
        } else {
          next(); // Jeśli nie, pozwól przejść do strony logowania
        }
      },
    },
    {
      path: "/home",
      name: "Home",
      component: Home,
      beforeEnter: (to, from, next) => {
        if (isLoggedIn()) {
          next();
        } else {
          next("/login");
        }
      },
    },
    {
      path: "/register",
      name: "Register",
      component: Register,
      beforeEnter: (to, from, next) => {
        if (isLoggedIn()) {
          next("/home");
        } else {
          next();
        }
      },
    },
    {
      path: "/",
      redirect: "/home", // Domyślnie przekierowanie na /home
    },
  ],
});

export default router;
