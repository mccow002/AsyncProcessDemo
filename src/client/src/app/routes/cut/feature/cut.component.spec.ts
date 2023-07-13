import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CutComponent } from './cut.component';

describe('CutComponent', () => {
  let component: CutComponent;
  let fixture: ComponentFixture<CutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CutComponent]
    });
    fixture = TestBed.createComponent(CutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
