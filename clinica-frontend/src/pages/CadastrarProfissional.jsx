import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function CadastrarProfissional() {
    const [nome, setNome] = useState('');
    const [especialidade, setEspecialidade] = useState('');
    const [erro, setErro] = useState('');
    const [sucesso, setSucesso] = useState('');
    const navigate = useNavigate();

    const handleCadastrar = async (e) => {
        e.preventDefault();
        setErro('');
        setSucesso('');
        
        const token = localStorage.getItem('token');
        
        try {
            await api.post('/Profissionais', { nome, especialidade }, {
                headers: { Authorization: `Bearer ${token}` }
            });
            setSucesso('Profissional cadastrado com sucesso!');
            setNome('');
            setEspecialidade('');
        } catch (error) {
            setErro('Erro ao cadastrar profissional. Verifique os dados.');
        }
    };

    return (
        <div style={{ padding: '30px', maxWidth: '400px', margin: '0 auto' }}>
            <h2>Cadastrar Profissional</h2>
            
            <form onSubmit={handleCadastrar} style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
                <input
                    type="text"
                    placeholder="Nome do Profissional"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    required
                    style={{ padding: '10px' }}
                />
                <input
                    type="text"
                    placeholder="Especialidade (ex: Cardiologia)"
                    value={especialidade}
                    onChange={(e) => setEspecialidade(e.target.value)}
                    required
                    style={{ padding: '10px' }}
                />
                <button type="submit" style={{ padding: '10px', cursor: 'pointer', backgroundColor: '#007BFF', color: 'white', border: 'none', borderRadius: '4px' }}>
                    Salvar Profissional
                </button>
            </form>

            {sucesso && <p style={{ color: 'green', marginTop: '15px' }}>{sucesso}</p>}
            {erro && <p style={{ color: 'red', marginTop: '15px' }}>{erro}</p>}

            <button onClick={() => navigate('/agendamentos')} style={{ marginTop: '20px', width: '100%', padding: '10px' }}>
                Voltar para Agendamentos
            </button>
        </div>
    );
}