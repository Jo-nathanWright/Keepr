import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepService {
  async createVaultKeep(vaultKeepBody) {
    try {
      const res = await api.post('api/vaultkeeps', vaultKeepBody)
      logger.log(res.data)
      AppState.vaultKeeps.push(res.data)
    } catch (error) {
      logger.log(error, 'error')
    }
  }

  async delete(vaultKeepId) {
    try {
      await api.delete('api/vaultkeeps/' + vaultKeepId)
      AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id !== vaultKeepId)
    } catch (error) {
      logger.log(error, 'error')
    }
  }
}

export const vaultKeepService = new VaultKeepService()
