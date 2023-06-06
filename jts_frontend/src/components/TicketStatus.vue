<template>
  <div class="flex justify-center items-center h-full">
    <div
      class="flex justify-center items-center rounded-lg h-5 w-20 text-center text-white"
      :class="setStatusColor(params.value)"
    >
      <p>{{ params.value }}</p>
    </div>
  </div>
</template>
<script>
export default {
  setup() {
    function setStatusColor(status) {
      switch (status) {
        case "Pending":
          return "bg-orange";
        case "Approved":
          return "bg-primary";
        case "Declined":
          return "bg-red";
      }
    }
    return {
      setStatusColor,
    };
  },
};
</script>
