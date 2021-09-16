import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId)
    logger.log(res.data)
    AppState.profile = res.data
  }

  async getVaultsByProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/vaults')
    logger.log(res.data)
    logger.log('Got vaults')
    AppState.profileVaults = res.data
  }

  async getKeepsByProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    logger.log(res.data)
    AppState.profileKeeps = res.data
  }
}

export const profileService = new ProfileService()
