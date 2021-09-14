<template>
  <div class="container-fluid mt-5">
    <div class="row"></div>
    <div class="card-columns">
      <Card v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await vaultsService.getKeeps(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.activeVault)
    }
  }
}
</script>

<style scoped>
</style>
