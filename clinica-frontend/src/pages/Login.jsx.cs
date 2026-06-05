import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function Login() {
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [erro, setErro] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            // Chama o endpoint da nossa API
            const response = await api.post('/Auth/login', { email, senha });
      
            // Salva o token gerado no Local Storage do navegador
            localStorage.setItem('token', response.data.token);
      
            // Redireciona para a tela de agendamentos (que vamos criar a seguir)
            navigate('/agendamentos');
        } catch (error) {
            setErro('Usuário ou senha inválidos.');
        }
    };

    return (
        <div style={{ padding: '50px', maxWidth: '400px', margin: '0 auto', textAlign: 'center' }}>
        <h2>Login - Clínica</h2>
        <form onSubmit={handleLogin} style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
    <input 
        type="email" 
    placeholder="E-mail" 
    value={email} 
    onChange={(e) => setEmail(e.target.value)} 
    required 
        />
        <input 
        type="password" 
    placeholder="Senha" 
    value={senha} 
    onChange={(e) => setSenha(e.target.value)} 
    required 
        />
        <button type="submit">Entrar</button>
        </form>
    {erro && <p style={{ color: 'red' }}>{erro}</p>}
    </div>
        );
}