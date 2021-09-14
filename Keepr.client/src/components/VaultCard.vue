<template>
  <router-link :to="{ name: 'Vault', params: {id: vault.id} }">
    <div v-if="vault.creatorId == account.id || vault.isPrivate == false">
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
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
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
