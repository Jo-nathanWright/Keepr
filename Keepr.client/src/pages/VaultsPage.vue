<template>
  <div class="container-fluid mt-5">
    <div class="row justify-content-center">
      <div class="col-11">
        <h1>{{ vault.name }}</h1>
        <h5>{{ vault.description }}</h5>
        <h5>Keeps : {{ keeps.length }}</h5>
      </div>
    </div>
    <div class="row justify-content-center mt-4">
      <div class="col-11">
        <div class="card-columns">
          <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
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
        await vaultsService.GetById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>

<style scoped>
</style>
