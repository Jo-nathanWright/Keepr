<template>
  <a class="dropdown-item" @click="create(vault.id)">{{ vault.name }}</a>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { vaultKeepService } from '../services/VaultKeepService'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'DropDown',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      newVaultKeep: {},
      editedKeep: {}
    })
    return {
      state,
      async create(vaultId) {
        try {
          const keep = AppState.activeKeep
          state.newVaultKeep.vaultId = vaultId
          state.newVaultKeep.keepId = keep.id
          await vaultKeepService.createVaultKeep(state.newVaultKeep)
          Pop.toast('Added to Vault!')
          state.editedKeep.keeps = keep.keeps + 1
          await keepsService.editViewsorKeeps(keep.id, state.editedKeep)
          await keepsService.getAll()
        } catch (error) {
          Pop.toast(error)
        }
      }
    }
  }
}
</script>
