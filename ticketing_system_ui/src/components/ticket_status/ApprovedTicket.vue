<template>
  <div class="flex">
    <input v-model="something" /> <button @click="add()">Add</button>
  </div>

  <div v-for="(item, index) in items">
    {{ item }} <button @click="remove(index)">remove</button>
  </div>
</template>
<script>
export default {
  data() {
    return {
      something: "",
      items: [],
    };
  },
  methods: {
    add() {
      this.items.push(this.something);
    },
    remove(i) {
      this.items.splice(i, 1);
    },
  },
};
</script>
