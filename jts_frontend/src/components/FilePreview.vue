<template>
  <component :is="tag" class="file-preview">
    <button @click="$emit('remove', file)" class="close-icon">&times;</button>
    <img :src="file.url" :alt="file.file.name" :title="file.file.name" />

    <!--  <span
      class="status-indicator loading-indicator"
      v-show="file.status == 'loading'"
      >In Progress</span
    >
    <span
      class="status-indicator success-indicator"
      v-show="file.status == true"
      >Uploaded</span
    >
    <span
      class="status-indicator failure-indicator"
      v-show="file.status == false"
      >Error</span
    > -->
  </component>
</template>

<script>
export default {
  props: {
    file: {},
    tag: "li",
  },
  emits: ["remove"],
  setup(props) {
    console.log(props.file.file);
  },
};
</script>

<style scoped>
.file-preview {
  width: 20%;
  margin: 1rem;
  position: relative;
  overflow: hidden;

  img {
    width: 100%;
    height: 100%;
    display: block;
    object-fit: cover;
  }

  .close-icon,
  .status-indicator {
    --size: 20px;
    position: absolute;
    line-height: var(--size);
    height: var(--size);
    border-radius: var(--size);
    box-shadow: 0 0 5px currentColor;
    right: 0.25rem;
    appearance: none;
    border: 0;
    padding: 0;
  }

  .close-icon {
    width: var(--size);
    font-size: var(--size);
    background: #933;
    color: #fff;
    top: 0.25rem;
    cursor: pointer;
  }

  .status-indicator {
    font-size: calc(0.75 * var(--size));
    bottom: 0.25rem;
    padding: 0 10px;
  }
}
</style>
