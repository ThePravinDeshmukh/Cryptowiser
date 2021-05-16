export default function renderCryptoRatesTable(symbol: string, symbolRates: ISymbolRate[]) {

    const classes = useStyles();
  
    return (
      <div>
        <br/>
        <p><em>Showing Rates for {symbol}</em></p> 
        <br/>
  
      
        <List className={classes.root}>
          {symbolRates.map((symbolRate) => (
                  <ListItem>
                  <ListItemAvatar>
                    <Avatar>
                    </Avatar>
                  </ListItemAvatar>
                  <ListItemText primary={symbolRate.name} secondary={symbolRate.priceDetail.price} />
                  </ListItem>
                ))}
        </List>
  
      </div>
    );
  }