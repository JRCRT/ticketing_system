<template>
  <div class="sidebar" :class="{ active: active }">
    <transition name="fade">
      <div
        class="bg-black w-full h-full opacity-60 absolute cursor-pointer"
        v-show="active"
        @click="$emit('close')"
      ></div>
    </transition>

    <transition name="slide">
      <div
        class="w-3/4 max-w-xs bg-white h-full z-10 overflow-x-scroll"
        v-show="active"
      >
        <div class="p-10" @click="$emit('close')">
          <slot name="header"></slot>
        </div>
        <div @click="$emit('close')">
          <slot name="content"></slot>
        </div>
      </div>
    </transition>
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
