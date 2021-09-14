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

  async Create(keepBody) {
    const res = await api.post('/api/keeps', keepBody)
    logger.log(res.data)
    AppState.keeps.push(res.data)
  }

  async Delete(keepId) {
    await api.delete('api/keeps/' + keepId)
  }
}

export const keepsService = new KeepsService()
