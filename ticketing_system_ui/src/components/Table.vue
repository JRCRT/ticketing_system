<script>
export default {
  data() {
    return {
      items: [
        {
          name: "Ryadsdsdsadsadasdsdsdsadasdsdsdsdsdsadsdsdsadsdn",
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
  mounted() {
    var thElm;
    var startOffset;

    Array.prototype.forEach.call(
      document.querySelectorAll("table th"),
      function (th) {
        th.style.position = "relative";

        var grip = document.createElement("div");
        grip.innerHTML = "&nbsp;";
        grip.style.top = 0;
        grip.style.right = 0;
        grip.style.bottom = 0;
        grip.style.width = "5px";
        grip.style.position = "absolute";
        grip.style.cursor = "col-resize";
        grip.addEventListener("mousedown", function (e) {
          thElm = th;
          startOffset = th.offsetWidth - e.pageX;
        });

        th.appendChild(grip);
      }
    );

    document.addEventListener("mousemove", function (e) {
      if (thElm) {
        thElm.style.width = startOffset + e.pageX + "px";
      }
    });

    document.addEventListener("mouseup", function () {
      thElm = undefined;
    });
  },
};
</script>

<template>
  <div class="table w-full rounded-md border">
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
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M18.75 19.5l-7.5-7.5 7.5-7.5m-6 15L5.25 12l7.5-7.5"
          />
        </svg>
      </button>
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M15.75 19.5L8.25 12l7.5-7.5"
          />
        </svg>
      </button>
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        1
      </button>
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M8.25 4.5l7.5 7.5-7.5 7.5"
          />
        </svg>
      </button>
      <button
        class="button button-transparent button-icon button-icon-sm flex justify-center items-center my-auto"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M11.25 4.5l7.5 7.5-7.5 7.5m-6-15l7.5 7.5-7.5 7.5"
          />
        </svg>
      </button>
    </div>
  </div>
</template>
