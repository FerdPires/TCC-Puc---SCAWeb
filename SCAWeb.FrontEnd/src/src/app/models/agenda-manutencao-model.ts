export class AgendaManutencaoModel {
    public Registros: any;

    constructor() {
        this.Registros = this.LimpaRegistros();
    }

    LimpaRegistros() {
        return {
            Id: '',
            tipo_manutencao: null,
            status_agenda: null,
            data_manutencao: '',
            data_atualizacao: '',
            id_insumo: ''
        }
    }
}
