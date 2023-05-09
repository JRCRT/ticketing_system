<template>
  <p :class="setStatusColor(params.value)">{{ params.value }}</p>
</template>
<script>
export default {
  setup() {
    function setStatusColor(status) {
      switch (status) {
        case "Pending":
          return "text-orange";
        case "Approved":
          return "text-primary";
        case "Declined":
          return "text-red";
      }
    }
    return {
      setStatusColor,
    };
  },
};
</script>
