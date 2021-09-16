import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepService {
  async createVaultKeep(vaultKeepBody) {
    const res = await api.post('api/vaultkeeps', vaultKeepBody)
    logger.log(res.data)
    AppState.vaultKeeps.push(res.data)
  }

  async delete(vaultKeepId) {
    await api.delete('api/vaultkeeps/' + vaultKeepId)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id !== vaultKeepId)
  }
}

export const vaultKeepService = new VaultKeepService()
