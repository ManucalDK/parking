import { CellService } from 'src/app/services/admin/cell/cell.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.sass']
})
export class LayoutComponent implements OnInit {

  cells: any[] = [];

  constructor(private cellService: CellService ) { }

  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts() {
    this.cellService.getAllCells().subscribe((cells: Array<any>) => {
      this.cells = cells;
    });
  }

}
