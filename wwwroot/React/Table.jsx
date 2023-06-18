class Table extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { headers, data } = this.props;
        const table = {
            borderCollapse: 'collapse',
            backgroundColor: '#fff',
            borderRadius: '6px',
            overflow: 'hidden',
            width: '100%',
            margin: '0 auto',
            color: '#000',
            position: 'relative',
        }
        const ththead = {
            height: '60px',
            backgroundColor: '#E3EDF4',
            fontSize: '16px',
            textAlign: "center"
        }
        const trtd = {
            textAlign: 'center',
            height: '48px',
            borderBottom: '1px solid #252936',
            padding: '0 30px 0 30px',
        }

        return (
            <table style={table}>
                <thead>
                    <tr>
                        {headers.map((item, index) => (
                            <th style={ththead} key={index}>{item}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {data.map((row, rowIndex) => (
                        <tr style={trtd} key={rowIndex}>
                            {row.map((cell, cellIndex) => (
                                <td style={trtd} key={cellIndex}>{cell}</td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }
}