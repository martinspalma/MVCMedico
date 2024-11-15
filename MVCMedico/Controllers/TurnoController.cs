using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMedico.Context;
using MVCMedico.Models;

namespace MVCMedico.Controllers
{
    public class TurnoController : Controller
    {
        private readonly MedicoDatabaseContext _context;

        public TurnoController(MedicoDatabaseContext context)
        {
            _context = context;
        }

        // GET: Turno
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turnos.ToListAsync());
        }

        // GET: Turno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turno/Create
        /*public IActionResult Create()
        {
            return View();
        }
        */
        // metodo de copilot

        // Paso 1: Seleccionar Especialidad
        public IActionResult SelectEspecialidad()
        {
            ViewData["Especialidad"] = new SelectList(Enum.GetValues(typeof(Especialidad)));
            return View();
        }

        // Paso 2: Filtrar Prestadores por Especialidad
        [HttpPost]
        public IActionResult SelectPrestadorMedico(Especialidad especialidad)
        {
            var prestadores = _context.Medicos
                                      .Where(p => p.Especialidad == especialidad)
                                      .ToList();
            ViewData["Prestadores"] = new SelectList(prestadores, "Id", "Nombre");
            return View();
        }

        // Paso 3: Seleccionar PrestadorMedico
        [HttpPost]
        public IActionResult SelectCita(int prestadorMedicoId)
        {
            var citas = _context.Citas
                                .Where(c => c.PrestadorMedico.IdPrestador == prestadorMedicoId && c.estaDisponible && c.FechaHora >= DateTime.Now && c.FechaHora <= DateTime.Now.AddDays(7))
                                .ToList();
            ViewData["Citas"] = new SelectList(citas, "Id", "FechaHora");
            return View();
        }

        // Paso 4: Seleccionar Cita y Confirmar Turno
        [HttpPost]
        public async Task<IActionResult> ConfirmTurno(int citaId, int afiliadoId)
        {
            var cita = await _context.Citas.FindAsync(citaId);
            if (cita == null || !cita.estaDisponible)
            {
                return NotFound();
            }

            cita.estaDisponible = false;
            _context.Update(cita);

            var afiliado = await _context.Afiliados.FindAsync(afiliadoId);
            if (afiliado == null)
            {
                return NotFound();
            }

             var turno = new Turno
             {
                // Especialidad = cita.PrestadorMedico.Especialidad,
                // PrestadorMedicoId = cita.PrestadorMedicoId,
                // AfiliadoId = afiliadoId
             };

             _context.Add(turno);
             await _context.SaveChangesAsync();

             return RedirectToAction(nameof(Index));
         }
      
            //metodo para crear turno por especialidad

            public List<PrestadorMedico> ObtenerMedicosPorEspecialidad(Especialidad especialidad)
            {
                //List<PrestadorMedico> medicos= new List<PrestadorMedico>();
                return _context.Medicos.Where(m => m.Especialidad == especialidad).ToList();
                //return Medicos.Where(m => m.Especialidad == especialidad).ToList();
            }

            // POST: Turno/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("IdTurno")] Turno turno)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(turno);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(turno);
            }

            // GET: Turno/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var turno = await _context.Turnos.FindAsync(id);
                if (turno == null)
                {
                    return NotFound();
                }
                return View(turno);
            }

            // POST: Turno/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("IdTurno")] Turno turno)
            {
                if (id != turno.IdTurno)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(turno);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TurnoExists(turno.IdTurno))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(turno);
            }

            // GET: Turno/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var turno = await _context.Turnos
                    .FirstOrDefaultAsync(m => m.IdTurno == id);
                if (turno == null)
                {
                    return NotFound();
                }

                return View(turno);
            }

            // POST: Turno/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var turno = await _context.Turnos.FindAsync(id);
                if (turno != null)
                {
                    _context.Turnos.Remove(turno);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool TurnoExists(int id)
            {
                return _context.Turnos.Any(e => e.IdTurno == id);
            }
        }
    }

