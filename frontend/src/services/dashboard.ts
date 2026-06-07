import api from './api';
export const dashboardService = {
  getMetrics: () => api.get('/dashboard/metrics')
};
