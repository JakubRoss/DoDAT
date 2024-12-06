<script>
import FullCalendar from "@fullcalendar/vue3";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import listPlugin from "@fullcalendar/list";
import api from "@/axios";

export default {
  components: {
    FullCalendar,
  },
  data() {
    return {
      tasks: [], // Przechowywane zadania
      calendarOptions: {
        plugins: [dayGridPlugin, interactionPlugin, listPlugin],
        initialView: "dayGridMonth",
        headerToolbar: {
          left: "prev,next today",
          center: "title",
          right: "dayGridMonth,dayGridWeek, listWeek",
        },
        events: [], // Zdarzenia dla kalendarza
        dateClick: this.handleDateClick,
        eventClick: this.handleEventClick,
        displayEventTime: false,
      },
    };
  },
  methods: {
    handleDateClick(info) {
      alert(`Kliknąłeś w datę: ${info.dateStr}`);
      console.log("Informacje o zdarzeniu:", info);
    },
    async fetchTasks() {
      try {
        const response = await api.get("/api/tasks");
        this.tasks = response.data;

        // Mapowanie zadań na zdarzenia i przypisanie ich do kalendarza
        this.calendarOptions.events = this.tasks.map((task) => ({
          id: task.id,
          title: task.title,
          start: task.dueDate, // Zakładamy, że "dueDate" to data w formacie ISO
          allDay: false, // Wydarzenie całodniowe
          extendedProps: {
            description: task.description,
          },
        }));
      } catch (error) {
        console.error("Error fetching tasks:", error);
      }
    },
    // Wyświetlanie szczegółów klikniętego zadania
    handleEventClick(info) {
      alert(`Clicked on task: ${info.event.title}`);
      console.log(this.calendarOptions.events);
    },
  },
  mounted() {
    this.fetchTasks(); // Pobierz zadania po zamontowaniu komponentu
  },
};
</script>
<template>
  <FullCalendar :options="calendarOptions" />
</template>

<style scoped>
.fc-event {
  cursor: pointer !important;
}
</style>
