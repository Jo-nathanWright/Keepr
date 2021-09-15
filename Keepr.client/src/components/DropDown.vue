<template>
  <a class="dropdown-item" @click="getVault">{{ vault.name }}</a>
</template>

<script>
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
export default {
  name: 'DropDown',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async getVault() {
        try {
          await vaultsService.GetById(props.vault.id)
        } catch (error) {
          Pop.toast(error)
        }
      }
    }
  }
}
</script>
