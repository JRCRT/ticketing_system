<template>
  <Transition name="fade">
    <div class="modal">
      <div class="modal-backdrop"></div>
      <!-- <div v-if="isModalLoading" class="modal-container m-auto">
      <LoadingSpinner />
    </div> -->
      <div v-if="isModalLoading" class="mx-auto my-auto">
        <Loading />
      </div>
      <div v-else class="modal-container">
        <div class="modal-header">
          <slot name="header"></slot>
          <button
            @click="$emit('close')"
            class="button-icon button-icon-sm button-transparent"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
              fill="currentColor"
            >
              <!-- Icon: x-sm -->
              <path
                fill-rule="evenodd"
                d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                clip-rule="evenodd"
              />
            </svg>
          </button>
        </div>

        <div>
          <slot name="content"></slot>
        </div>
        <div class="modal-footer">
          <slot name="footer"></slot>
        </div>
      </div>
    </div>
  </Transition>
</template>

<script>
import { computed } from "vue";
import { useStore } from "vuex";
import LoadingSpinner from "@/components/LoadingSpinner.vue";
import Loading from "@/components/Loading.vue";
export default {
  emits: ["close"],
  mounted() {
    document.body.classList.add("prevent-scroll");
  },
  unmounted() {
    document.body.classList.remove("prevent-scroll");
  },
  components: {
    LoadingSpinner,
    Loading,
  },

  setup() {
    const store = useStore();
    const isModalLoading = computed(() => store.state.app.isModalLoading);

    return {
      isModalLoading,
    };
  },
};
</script>
