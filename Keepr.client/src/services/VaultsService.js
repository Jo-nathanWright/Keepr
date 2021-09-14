import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getKeeps(vaultId) {
    try {
      const res = await api('/api/vaults/' + vaultId + '/keeps')
      logger.log(res.data)
      AppState.activeVault = res.data
    } catch (error) {
      logger.log(error, 'error')
    }
  }
}

export const vaultsService = new VaultsService()
