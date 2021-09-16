import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAll() {
    const res = await api.get('/api/keeps')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async getById(keepId) {
    const res = await api.get('/api/keeps/' + keepId)
    logger.log(res.data)
    AppState.activeKeep = res.data
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
