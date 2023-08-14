<template>
  <div class="file-preview">
    <div class="image-container">
      <img :src="file?.url" :alt="file?.file?.name" :title="file?.file?.name" />
      <span>{{ file?.file?.name }}</span>
    </div>
    <button>Change</button>
  </div>
</template>

<script>
export default {
  props: {
    file: {},
  },
  emits: ["remove"],
};
</script>

<style scoped>
.file-preview {
  width: 150px;
  margin: 1rem;

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
</style>
