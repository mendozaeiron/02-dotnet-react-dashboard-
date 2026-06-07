import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const Login = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    
    try {
      const response = await api.post('/auth/login', { username, password });
      localStorage.setItem('token', response.data.token);
      navigate('/dashboard');
    } catch (err: any) {
      console.error('Login error:', err);
      setError(err.response?.data?.message || 'Error al iniciar sesi?n. Verifica que el backend est? corriendo.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', background: '#f5f5f5' }}>
      <form onSubmit={handleSubmit} style={{ padding: '30px', background: 'white', borderRadius: '8px', boxShadow: '0 2px 10px rgba(0,0,0,0.1)', width: '350px' }}>
        <h2 style={{ textAlign: 'center', marginBottom: '20px' }}>Iniciar Sesi?n</h2>
        {error && <div style={{ color: 'red', marginBottom: '10px', padding: '10px', background: '#ffeeee', borderRadius: '4px' }}>{error}</div>}
        <input
          type="text"
          placeholder="Usuario"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          style={{ width: '100%', padding: '10px', marginBottom: '10px', border: '1px solid #ddd', borderRadius: '4px', boxSizing: 'border-box' }}
          required
        />
        <input
          type="password"
          placeholder="Contrase?a"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          style={{ width: '100%', padding: '10px', marginBottom: '20px', border: '1px solid #ddd', borderRadius: '4px', boxSizing: 'border-box' }}
          required
        />
        <button 
          type="submit" 
          disabled={loading}
          style={{ width: '100%', padding: '10px', background: loading ? '#ccc' : '#007bff', color: 'white', border: 'none', borderRadius: '4px', cursor: loading ? 'not-allowed' : 'pointer' }}
        >
          {loading ? 'Cargando...' : 'Ingresar'}
        </button>
        <div style={{ marginTop: '15px', textAlign: 'center', fontSize: '12px', color: '#666' }}>
          Usuario: admin / Contrase?a: Admin123!
        </div>
      </form>
    </div>
  );
};

export default Login;
