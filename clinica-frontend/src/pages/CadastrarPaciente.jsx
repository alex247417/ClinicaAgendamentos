import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

export default function CadastrarPaciente() {
    const [nome, setNome] = useState('');
    const [cpf, setCpf] = useState('');
    const [erro, setErro] = useState('');
    const [sucesso, setSucesso] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const token = localStorage.getItem('token');
            await api.post('/Pacientes', { nome, cpf }, {
                headers: { Authorization: `Bearer ${token}` }
            });
            setSucesso('Paciente cadastrado com sucesso!');
            setNome('');
            setCpf('');
        } catch (error) {
            setErro('Erro ao cadastrar paciente.');
        }
    };

    return (
        <div style={{ padding: '50px', maxWidth: '400px', margin: '0 auto', textAlign: 'center' }}>
            <h2>Cadastrar Paciente</h2>
            <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
                <input
                    type="text"
                    placeholder="Nome completo"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    required
                />
                <input
                    type="text"
                    placeholder="CPF"
                    value={cpf}
                    onChange={(e) => setCpf(e.target.value)}
                    required
                />
                <button type="submit">Cadastrar</button>
                <button type="button" onClick={() => navigate('/agendamentos')}>
                    Voltar
                </button>
            </form>
            {sucesso && <p style={{ color: 'green' }}>{sucesso}</p>}
            {erro && <p style={{ color: 'red' }}>{erro}</p>}
        </div>
    );
}