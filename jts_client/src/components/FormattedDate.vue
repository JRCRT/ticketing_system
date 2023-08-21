<template>
  <p>{{ Intl.DateTimeFormat("en-US").format(new Date(params.value)) }}</p>
</template>
<script>
export default {};
</script>
