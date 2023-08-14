<template>
  <component :is="tag" class="file-preview">
    <button @click="$emit('remove', file)" class="close-icon">&times;</button>
    <div class="image-container">
      <img :src="file.url" :alt="file.file.name" :title="file.file.name" />
      <span>{{ file.file.name }}</span>
    </div>
  </component>
</template>

<script>
export default {
  props: {
    file: {},
    tag: "li",
  },
  emits: ["remove"],
};
</script>

<style scoped>
.file-preview {
  width: 150px;
  margin: 1rem;
  position: relative;
  overflow: hidden;
}
.image-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

span {
  font-size: 12px;
  text-align: center;
}

img {
  width: 80px;
  height: 80px;
  display: block;
  object-fit: cover;
}

.close-icon {
  width: var(--size);
  font-size: var(--size);
  background: #933;
  color: #fff;
  top: 0.25rem;
  cursor: pointer;
  --size: 20px;
  position: absolute;
  line-height: var(--size);
  height: var(--size);
  border-radius: var(--size);
  box-shadow: 0 0 5px currentColor;
  right: 1px;
  appearance: none;
  border: 0;
  padding: 0;
}
</style>
