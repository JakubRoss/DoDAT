import { createRouter, createWebHistory } from "vue-router";
import Login from "@/views/Login.vue";

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
          next("/"); // Jeśli zalogowany, przekieruj na stronę główną
        } else {
          next(); // Jeśli nie, pozwól przejść do strony logowania
        }
      },
    },
  ],
});

export default router;
