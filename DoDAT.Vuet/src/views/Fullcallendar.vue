<script>
import FullCalendar from "@fullcalendar/vue3";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import listPlugin from "@fullcalendar/list";

export default {
  components: {
    FullCalendar,
  },
  data() {
    return {
      calendarOptions: {
        plugins: [dayGridPlugin, interactionPlugin, listPlugin],
        initialView: "dayGridMonth",
        headerToolbar: {
          left: "prev,next today",
          center: "title",
          right: "dayGridMonth,dayGridWeek, listWeek",
        },
        dateClick: this.handleDateClick,
      },
    };
  },
  methods: {
    handleDateClick(info) {
      alert(`Kliknąłeś w datę: ${info.dateStr}`);
      console.log("Informacje o zdarzeniu:", info);
    },
  },
};
</script>
<template>
  <FullCalendar :options="calendarOptions" />
</template>
