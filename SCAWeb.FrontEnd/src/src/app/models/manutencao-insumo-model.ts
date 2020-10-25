export class ManutencaoInsumoModel {
    public Registros: any;
    public listTipoManutencao: Array<any>;

    constructor() {
        //  this.Parametros = this.LimpaParametro();
        this.Registros = this.LimpaRegistros();

        this.listTipoManutencao = [
            { value: '', text: '' },
            { value: 1, text: 'Preventiva' },
            { value: 2, text: 'Corretiva' }
        ];

    }

    // moment(new Date()).format("DD/MM/YYYY 00:00"),
    // moment(new Date()).format("DD/MM/YYYY 23:59"),

    LimpaRegistros() {
        return {
            Id: '',
            tipo_manutencao: null,
            descricao_manutencao: '',
            status_manutencao: null,
            data_inicio: '',
            data_fim: '',
            id_insumo: ''
        }
    }
}
