<template>
  <a class="dropdown-item">{{ vault.name }}</a>
</template>

<script>
export default {
  name: 'DropDown',
  props: {
    vault: {
      type: Object,
      required: true
    }
  }
}
</script>
