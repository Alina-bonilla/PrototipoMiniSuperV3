import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RankingSalarioComponent } from './ranking-salario.component';

describe('RankingSalarioComponent', () => {
  let component: RankingSalarioComponent;
  let fixture: ComponentFixture<RankingSalarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RankingSalarioComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RankingSalarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
