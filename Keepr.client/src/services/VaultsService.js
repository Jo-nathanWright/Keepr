import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async GetById(vaultId) {
    const res = await api.get('/api/vaults/' + vaultId)
    logger.log(res.data)
    AppState.activeVault = res.data
  }

  async getKeeps(vaultId) {
    const res = await api.get('/api/vaults/' + vaultId + '/keeps')
    logger.log(res.data)
    AppState.vaultKeeps = res.data
  }

  async createVault(vaultBody) {
    const res = await api.post('api/vaults', vaultBody)
    logger.log(res.data)
    AppState.profileVaults.push(res.data)
  }

  async deleteVault(vaultId) {
    await api.delete('api/vaults/' + vaultId)
  }
}

export const vaultsService = new VaultsService()
