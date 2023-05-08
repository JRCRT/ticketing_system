<template>
  <div class="sidebar" :class="{ active: active }">
    <Transition name="fade">
      <div class="sidebar-backdrop" v-if="active" @click="$emit('close')"></div>
    </Transition>

    <Transition name="slide">
      <div class="sidebar-content" v-if="active">
        <div class="p-10" @click="$emit('close')">
          <slot name="header"></slot>
        </div>
        <div @click="$emit('close')">
          <slot name="content"></slot>
        </div>
      </div>
    </Transition>
  </div>
</template>

<script>
export default {
  props: {
    active: false,
  },
  emits: ["close"],
  watch: {
    active: (value) => {
      value
        ? document.body.classList.add("prevent-scroll")
        : document.body.classList.remove("prevent-scroll");
    },
  },
};
</script>
