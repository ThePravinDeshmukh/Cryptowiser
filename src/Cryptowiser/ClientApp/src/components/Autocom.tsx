import React, { ChangeEvent, Component } from 'react';  
import TextField from '@material-ui/core/TextField';  
import Autocomplete from '@material-ui/lab/Autocomplete';  
import axios from 'axios';  
import { authHeader } from '../_helpers/auth-header';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import { Spinner } from 'reactstrap';

interface IProps {
}

interface ISymbolRate{
  name: string;
  priceDetail: IPriceDetail ;
}
interface IPriceDetail{
  price: number;
  lastUpdated: Date ;
}
interface IAutocomState {
    Symbols: string[];
    Symbol: string;
    rates: ISymbolRate[];
    errorMessage: string;
}


export class Autocom extends Component<IProps, IAutocomState> { 
        constructor(props: IProps) {  
          super(props)  
          this.state = {  
                  Symbols: [],
                  Symbol: "",
                  rates: [],
                  errorMessage: ""
          }
          
          this.onCurrencyChange = this.onCurrencyChange.bind(this);
        }  


        
        componentDidMount() {  
            const headers = authHeader();
              axios.get('api/crypto/symbols/market_cap', { headers })
              .then(response => {
                this.setState({  
                    Symbols: response.data  
                });  
              })
              .catch(error => {
                  console.error('There was an error!', error);
              });

        }  

        onCurrencyChange = (event: ChangeEvent<{}>, values: string | null) => {
            this.setState({
                Symbol: values ?? ""
              }, () => {
                console.log(this.state.Symbol);
                this.populateCryptoRates(this.state.Symbol);
            });
          }
          

        static renderCryptoRatesTable(symbol: string, symbolRates: ISymbolRate[]) {



          return (
            <div>
              <br/>
              <p><em>Showing Rates for {symbol}</em></p> 
              <br/>

              <TableContainer component={Paper}>
                <Table  aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <TableCell >Price</TableCell>
                    <TableCell  align="right">Last Updated</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {symbolRates.map((symbolRate) => (
                    <TableRow key={symbolRate.name}>
                      <TableCell >{symbolRate.priceDetail.price} {symbolRate.name}</TableCell>
                      <TableCell align="right">{symbolRate.priceDetail.lastUpdated}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer> 
            </div>
          );
        }

        render() {  
                
          let contents = this.state.Symbol
          ? Autocom.renderCryptoRatesTable(this.state.Symbol , this.state.rates)
          : <p><em>Please Search and Select Crypto Currency Above!</em></p>;
          
          let refreshLink = this.state.Symbol ?
          <p><a href="#" onClick={e=> this.populateCryptoRates(this.state.Symbol)}>Refresh</a></p>
          : null;

          return (  
            <div>  
              <table>
                <tr>
                  <td>
                    <Autocomplete className="pding"  
                      id="combo-box-demo"  
                      onChange={this.onCurrencyChange}
                      options={this.state.Symbols}  
                      getOptionLabel={option => option}  
                      style={{ width: 300 }}  
                      renderInput={params => (  
                        <TextField {...params} label="Search Crypto Currency" variant="outlined" fullWidth /> 
                      )}  
                    />
                  </td>
                  <td> {refreshLink}</td>
                  </tr>
              </table>

              {contents}
            </div>  
            )  
        }
        
        async populateCryptoRates(symbol: string) {
            const headers = authHeader();
        
            axios.get('api/crypto/rates/' + symbol, { headers })
            .then(response => {
              this.setState({ rates: response.data});
            })
            .catch(error => {
                this.setState({ errorMessage: error.message });
                console.error('There was an error!', error);
            });
          }
}  
export default Autocom  



