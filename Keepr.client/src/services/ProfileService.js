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
}

export const profileService = new ProfileService()
