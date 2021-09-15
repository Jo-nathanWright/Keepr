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

  async getById(keepId) {
    try {
      const res = await api.get('/api/keeps/' + keepId)
      logger.log(res.data)
      AppState.activeKeep = res.data
    } catch (error) {
      logger.log('Error', error)
    }
  }

  async Create(keepBody) {
    try {
      const res = await api.post('/api/keeps', keepBody)
      logger.log(res.data)
      AppState.keeps.push(res.data)
    } catch (error) {
      logger.log('Error ', error)
    }
  }

  async Delete(keepId) {
    try {
      await api.delete('api/keeps/' + keepId)
    } catch (error) {
      logger.log('Error ', error)
    }
  }
}

export const keepsService = new KeepsService()
