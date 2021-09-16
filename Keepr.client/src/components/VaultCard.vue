<template>
  <router-link :to="{ name: 'Vault', params: {vaultId: vault.id} }">
    <div>
      <div class="card">
        <img class="card-img-top" :src="vault.img" alt="Card image cap">
        <div class="card-img-overlay text-light d-flex align-items-end justify-content-between">
          <h5 class="card-title">
            {{ vault.name }}
          </h5>
        </div>
      </div>
    </div>
  </router-link>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    onMounted(async() => {
      try {
        await vaultsService.GetById(props.vault.id)
      } catch (error) {
        Pop.toast(error)
      }
    })
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
.image{
  max-height: 50px;
}
</style>
