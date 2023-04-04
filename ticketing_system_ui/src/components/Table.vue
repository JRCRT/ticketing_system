<script>
export default {
  data() {
    return {
      items: [
        {
          name: "Ryan",
          age: 24,
          gender: "Male",
        },
        {
          name: "Mark",
          age: 23,
          gender: "Male",
        },
      ],
      headers: [
        {
          name: "Name",
        },
        {
          name: "Age",
        },
        {
          name: "Gender",
        },
        {
          name: "Actions",
        },
      ],
    };
  },
};
</script>

<template>
  <div class="w-full rounded-md border">
    <table class="w-full">
      <thead>
        <th class="border" v-for="(header, index) in headers" :key="index">
          {{ header.name }}
        </th>
      </thead>
      <tbody>
        <tr class="border-b" v-for="(item, index) in items">
          <td class="py-2 pl-4" v-for="i in item">{{ i }}</td>
        </tr>
      </tbody>
    </table>
    <div class="flex p-1">
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        Prev
      </button>
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        1
      </button>
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        Next
      </button>
    </div>
  </div>
</template>
