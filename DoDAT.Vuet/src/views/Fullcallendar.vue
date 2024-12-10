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
        editable: true, // Włącza drag-and-drop dla wydarzeń
        droppable: true, // Pozwala przeciągać elementy spoza kalendarza
        eventDrop: this.handleEventDrop, // Funkcja obsługująca przeciągnięcie wydarzenia
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
            isCompleted: task.isCompleted,
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
    // Obsługa przeciągania wydarzeń
    async handleEventDrop(info) {
      const originalDate = new Date(info.event.start); // Pobieramy oryginalną datę
      originalDate.setDate(originalDate.getDate() + 1); // Dodajemy jeden dzień

      // Konwertujemy datę na format yyyy-mm-dd
      const updatedStartDate = originalDate.toISOString().split("T")[0];

      const eventId = info.event.id;
      console.log(updatedStartDate);
      console.log(info.event.id);
      try {
        const response = await api.put(`/api/tasks/${eventId}`, {
          title: info.event.title,
          description: info.event.description,
          isCompleted: info.event.isCompleted,
          dueDate: updatedStartDate, // Przesyłamy tylko zaktualizowaną datę
        });

        // Sprawdzenie odpowiedzi z serwera
        if (response.status === 200) {
          alert(`Event ${eventId} updated to ${updatedStartDate}`);
        } else {
          // Jeśli aktualizacja nie powiedzie się, możemy przywrócić pozycję
          alert("Błąd aktualizacji wydarzenia.");
          info.revert(); // Cofnij zmianę, jeśli coś poszło nie tak
        }
      } catch (error) {
        console.error("Error updating event:", error);
        // Jeśli wystąpi błąd, możemy cofnąć zmianę
        info.revert(); // Cofnij zmianę, jeśli wystąpi błąd
        alert("Wystąpił błąd podczas aktualizacji.");
      }
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
