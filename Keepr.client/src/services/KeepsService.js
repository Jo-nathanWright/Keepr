import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAll() {
    try {
      const res = await api.get('/api/keeps')
      logger.log(res.data)
      AppState.keeps = res.data
    } catch (err) {
      logger.log('Error ', err)
    }
  }

  async Delete(keepId) {
    await api.delete('api/keeps/' + keepId)
    AppState.keeps = AppState.keeps.filter(k => k.id !== keepId)
  }
}

export const keepsService = new KeepsService()
