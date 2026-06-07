import { useState, useEffect } from 'react';
import api from '../services/api';

const Dashboard = () => {
  const [metrics, setMetrics] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const token = localStorage.getItem('token');
        if (!token) {
          window.location.href = '/login';
          return;
        }
        const response = await api.get('/Dashboard/metrics');
        setMetrics(response.data);
      } catch (err: any) {
        console.error('Error:', err);
        setError(err.message || 'Error al cargar datos');
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('token');
    window.location.href = '/login';
  };

  if (loading) return <div style={{ padding: '20px', textAlign: 'center' }}>Cargando...</div>;
  if (error) return <div style={{ padding: '20px', textAlign: 'center', color: 'red' }}>Error: {error}</div>;
  if (!metrics) return <div style={{ padding: '20px', textAlign: 'center' }}>No hay datos</div>;

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h1>Dashboard</h1>
        <button 
          onClick={handleLogout}
          style={{ padding: '8px 16px', background: '#dc3545', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}
        >
          Cerrar Sesi?n
        </button>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '20px', marginBottom: '20px' }}>
        <div style={{ padding: '20px', border: '1px solid #ddd', borderRadius: '8px', background: '#f9f9f9' }}>
          <h3>Usuarios Activos</h3>
          <p style={{ fontSize: '28px', fontWeight: 'bold', margin: '10px 0', color: '#007bff' }}>
            {metrics.activeUsers?.value || 0}
          </p>
          <small style={{ color: metrics.activeUsers?.isPositiveTrend ? 'green' : 'red' }}>
            {metrics.activeUsers?.trend > 0 ? '+' : ''}{metrics.activeUsers?.trend}%
          </small>
        </div>

        <div style={{ padding: '20px', border: '1px solid #ddd', borderRadius: '8px', background: '#f9f9f9' }}>
          <h3>Ingresos Totales</h3>
          <p style={{ fontSize: '28px', fontWeight: 'bold', margin: '10px 0', color: '#28a745' }}>
            ${metrics.totalRevenue?.value?.toLocaleString() || 0}
          </p>
          <small style={{ color: metrics.totalRevenue?.isPositiveTrend ? 'green' : 'red' }}>
            {metrics.totalRevenue?.trend > 0 ? '+' : ''}{metrics.totalRevenue?.trend}%
          </small>
        </div>

        <div style={{ padding: '20px', border: '1px solid #ddd', borderRadius: '8px', background: '#f9f9f9' }}>
          <h3>Tasa Conversi?n</h3>
          <p style={{ fontSize: '28px', fontWeight: 'bold', margin: '10px 0', color: '#ffc107' }}>
            {metrics.conversionRate?.value || 0}%
          </p>
          <small style={{ color: metrics.conversionRate?.isPositiveTrend ? 'green' : 'red' }}>
            {metrics.conversionRate?.trend > 0 ? '+' : ''}{metrics.conversionRate?.trend}%
          </small>
        </div>

        <div style={{ padding: '20px', border: '1px solid #ddd', borderRadius: '8px', background: '#f9f9f9' }}>
          <h3>Tiempo Carga</h3>
          <p style={{ fontSize: '28px', fontWeight: 'bold', margin: '10px 0', color: '#dc3545' }}>
            {metrics.pageLoadTime?.value || 0}s
          </p>
          <small style={{ color: metrics.pageLoadTime?.isPositiveTrend ? 'green' : 'red' }}>
            {metrics.pageLoadTime?.trend > 0 ? '+' : ''}{metrics.pageLoadTime?.trend}%
          </small>
        </div>
      </div>

      <div style={{ padding: '20px', border: '1px solid #ddd', borderRadius: '8px', background: '#f9f9f9' }}>
        <h3>Actividades Recientes</h3>
        <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '10px' }}>
          <thead>
            <tr>
              <th style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>Usuario</th>
              <th style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>Acci?n</th>
              <th style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>Fecha</th>
            </tr>
          </thead>
          <tbody>
            {metrics.recentActivities && metrics.recentActivities.map((activity: any, idx: number) => (
              <tr key={idx}>
                <td style={{ padding: '8px', borderBottom: '1px solid #eee' }}>{activity.user}</td>
                <td style={{ padding: '8px', borderBottom: '1px solid #eee' }}>{activity.action}</td>
                <td style={{ padding: '8px', borderBottom: '1px solid #eee' }}>{new Date(activity.timestamp).toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Dashboard;
