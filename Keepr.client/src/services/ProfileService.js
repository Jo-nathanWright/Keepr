import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfileService {
  async getProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId)
      logger.log(res.data)
      AppState.profile = res.data
    } catch (error) {
      logger.log('Error', error)
    }
  }

  async getVaultsByProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      logger.log(res.data)
      logger.log('Got vaults')
      AppState.profileVaults = res.data
    } catch (error) {
      logger.log('Error', error)
    }
  }

  async getKeepsByProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      logger.log(res.data)
      AppState.profileKeeps = res.data
    } catch (error) {
      logger.log('Error', error)
    }
  }
}

export const profileService = new ProfileService()
