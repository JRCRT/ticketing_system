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
import { TICKET_STATUS } from "@/util/constant";
export default {
  setup() {
    function setStatusColor(status) {
      switch (status) {
        case TICKET_STATUS.PENDING:
          return "bg-orange";
        case TICKET_STATUS.APPROVED:
          return "bg-blue";
        case TICKET_STATUS.DECLINED:
          return "bg-red";
        case TICKET_STATUS.DONE:
          return "bg-primary";
      }
    }
    return {
      setStatusColor,
    };
  },
};
</script>
