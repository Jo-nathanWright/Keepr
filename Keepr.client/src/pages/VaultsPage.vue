<template>
  <div class="container-fluid mt-5">
    <div class="row justify-content-center">
      <div class="col-11">
        <div class="d-flex align-items-center">
          <h1>{{ vault.name }}</h1>
          <h3 v-if="vault.creatorId === account.id" class="ml-5 action" @click="destroy(vault.id, vault.creatorId)">
            🗑
          </h3>
        </div>
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
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      editedKeep: {}
    })
    onMounted(async() => {
      try {
        await vaultsService.GetById(route.params.vaultId)
        await vaultsService.getKeeps(route.params.vaultId)
      } catch (error) {
        router.push({ name: 'Home' })
      }
    })
    return {
      state,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
      async destroy(vaultId, userId) {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(vaultId)
            await profileService.getVaultsByProfile(userId)
            router.push({ name: 'Profile', params: { id: userId } })
            Pop.toast('That vault has been Deleted')
          }
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
.action{
  cursor: pointer;
}
</style>
